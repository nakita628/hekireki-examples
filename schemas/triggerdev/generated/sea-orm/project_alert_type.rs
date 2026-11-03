use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "ProjectAlertType")]
pub enum ProjectAlertType {
    #[sea_orm(string_value = "TASK_RUN")]
    TaskRun,
    #[sea_orm(string_value = "TASK_RUN_ATTEMPT")]
    TaskRunAttempt,
    #[sea_orm(string_value = "DEPLOYMENT_FAILURE")]
    DeploymentFailure,
    #[sea_orm(string_value = "DEPLOYMENT_SUCCESS")]
    DeploymentSuccess,
    #[sea_orm(string_value = "ERROR_GROUP")]
    ErrorGroup,
    #[sea_orm(string_value = "DASHBOARD_AGENT_WATCH")]
    DashboardAgentWatch,
}
