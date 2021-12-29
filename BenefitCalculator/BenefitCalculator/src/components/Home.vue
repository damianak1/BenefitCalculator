<template>
    <div class="home">
        <div class="row">
            <div class="col-sm"></div>
            <div class="col-sm mt-5">
                <form @submit="calculateBenefits">
                    <div class="form-group">
                        <label>Participant Name</label>
                        <input type="text" v-model="model.ParticipantBenefitRecord.Name" class="form-control mb-3" required />
                    </div>
                    <div class="form-group">
                        <button type="button" class="btn btn-primary mb-3" @click="addClick()"> Add Dependent </button>
                    </div>
                    <div v-for="(dependent, index) in model.DependencyBenefitRecords" :key="index">
                        <div class="form-group">
                            <div class="input-group w-50 mb-3">

                                <input type="text" class="form-control" placeholder="Enter name of dependent" v-model="dependent.Name">
                                <span class="input-group-text" @click="removeClick(index)">
                                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-trash" viewBox="0 0 16 16">
                                        <path d="M5.5 5.5A.5.5 0 0 1 6 6v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5zm2.5 0a.5.5 0 0 1 .5.5v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5zm3 .5a.5.5 0 0 0-1 0v6a.5.5 0 0 0 1 0V6z"></path>
                                        <path fill-rule="evenodd" d="M14.5 3a1 1 0 0 1-1 1H13v9a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V4h-.5a1 1 0 0 1-1-1V2a1 1 0 0 1 1-1H6a1 1 0 0 1 1-1h2a1 1 0 0 1 1 1h3.5a1 1 0 0 1 1 1v1zM4.118 4 4 4.059V13a1 1 0 0 0 1 1h6a1 1 0 0 0 1-1V4.059L11.882 4H4.118zM2.5 3V2h11v1h-11z"></path>
                                    </svg>
                                </span>
                            </div>
                        </div>
                    </div>

                    <button type="submit" class="btn btn-primary"> Calculate Cost</button>
                    <div v-if="results.BenefitCostPerPayPeriod" class="mt-5">
                        Cost of Benefits per Pay Period: {{results.BenefitCostPerPayPeriod}}
                    </div>
                </form>
            </div>
            <div class="col-sm "></div>

        </div>
    </div>

</template>

<script>
    import axios from 'axios'

    /*
     {
        "ParticipantPayPerPeriod": 2000,
        "BaseParticipantBenefitAnnualCost": 1000,
        "BaseDependentBenefitAnnualCost": 500,
        "ParticipantBenefitRecord": {
            "Name": null,
            "DiscountPercent": 0,
            "DiscountPercentAsDecimal": 0
        },
        "DependencyBenefitRecords": []
      }

      {
          "BenefitCostPerPayPeriod": 38.46
      }
     */


    export default {
        name: 'Home',
        props: {},
        data() {
            return {
                model: {},
                results: {}
            }
        },
        methods: {
            refreshData() {
                axios.get("http://localhost:45365/api/BenefitCalculator")
                    .then((response) => {
                        this.model = response.data;
                    });
            },
            removeClick(index) {
                this.model.DependencyBenefitRecords.splice(index, 1);
            },
            addClick() {
                console.log("add called");
                this.model.DependencyBenefitRecords.push({ Name: "" });
            },
            calculateBenefits(e) {
                e.preventDefault();
                axios.post("http://localhost:45365/api/BenefitCalculator", this.model)
                    .then((response) => {
                        this.results = response.data;
                    });
            }
        },
        mounted: function () {
            this.refreshData();
        }
    };
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>


