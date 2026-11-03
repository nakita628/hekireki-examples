use super::worker_instance_group_type::WorkerInstanceGroupType;
use super::workload_type::WorkloadType;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "WorkerInstanceGroup")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub r#type: WorkerInstanceGroupType,
    pub name: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "masterQueue")]
    pub master_queue: String,
    pub description: Option<String>,
    #[sea_orm(default_value = false)]
    pub hidden: bool,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "tokenId")]
    pub token_id: String,
    #[sea_orm(column_name = "organizationId")]
    pub organization_id: Option<String>,
    #[sea_orm(column_name = "projectId")]
    pub project_id: Option<String>,
    #[sea_orm(column_name = "cloudProvider")]
    pub cloud_provider: Option<String>,
    pub location: Option<String>,
    #[sea_orm(column_name = "staticIPs")]
    pub static_ips: Option<String>,
    #[sea_orm(column_name = "workloadType", default_value = "CONTAINER")]
    pub workload_type: WorkloadType,
    pub region: Option<String>,
    #[sea_orm(column_name = "enableFastPath", default_value = false)]
    pub enable_fast_path: bool,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::worker_group_token::Entity",
        from = "Column::TokenId",
        to = "super::worker_group_token::Column::Id",
        on_update = "Cascade",
        on_delete = "Cascade"
    )]
    Token,
    #[sea_orm(
        belongs_to = "super::organization::Entity",
        from = "Column::OrganizationId",
        to = "super::organization::Column::Id",
        on_update = "Cascade",
        on_delete = "Cascade"
    )]
    Organization,
    #[sea_orm(
        belongs_to = "super::project::Entity",
        from = "Column::ProjectId",
        to = "super::project::Column::Id",
        on_update = "Cascade",
        on_delete = "Cascade"
    )]
    Project,
    #[sea_orm(has_many = "super::worker_instance::Entity")]
    Workers,
    #[sea_orm(has_many = "super::background_worker::Entity")]
    BackgroundWorkers,
    #[sea_orm(
        has_many = "super::project::Entity",
        from = "Column::Id",
        to = "super::project::Column::DefaultWorkerGroupId"
    )]
    DefaultForProjects,
}

impl Related<super::worker_group_token::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Token.def()
    }
}

impl Related<super::organization::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Organization.def()
    }
}

impl Related<super::project::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Project.def()
    }
}

impl Related<super::worker_instance::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Workers.def()
    }
}

impl Related<super::background_worker::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::BackgroundWorkers.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.created_at.is_not_set() {
            self.created_at = Set(now.naive_utc());
        }
        if !self.updated_at.is_set() {
            self.updated_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
