use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "LiteLLM_UserTable")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub user_id: String,
    pub user_alias: Option<String>,
    pub team_id: Option<String>,
    #[sea_orm(unique)]
    pub sso_user_id: Option<String>,
    pub organization_id: Option<String>,
    pub object_permission_id: Option<String>,
    pub password: Option<String>,
    pub password_reset_required: Option<bool>,
    pub last_breach_check_at: Option<DateTime>,
    pub teams: Vec<String>,
    pub user_role: Option<String>,
    pub max_budget: Option<f64>,
    #[sea_orm(default_value = 0)]
    pub spend: f64,
    pub user_email: Option<String>,
    pub models: Vec<String>,
    #[sea_orm(default_value = "{}")]
    pub metadata: Json,
    pub max_parallel_requests: Option<i32>,
    pub tpm_limit: Option<i64>,
    pub rpm_limit: Option<i64>,
    pub budget_duration: Option<String>,
    pub budget_reset_at: Option<DateTime>,
    pub allowed_cache_controls: Vec<String>,
    pub policies: Vec<String>,
    #[sea_orm(default_value = "{}")]
    pub model_spend: Json,
    #[sea_orm(default_value = "{}")]
    pub model_max_budget: Json,
    pub created_at: Option<DateTime>,
    pub updated_at: Option<DateTime>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::lite_llm_organization_table::Entity",
        from = "Column::OrganizationId",
        to = "super::lite_llm_organization_table::Column::OrganizationId"
    )]
    LitellmOrganizationTable,
    #[sea_orm(
        belongs_to = "super::lite_llm_object_permission_table::Entity",
        from = "Column::ObjectPermissionId",
        to = "super::lite_llm_object_permission_table::Column::ObjectPermissionId"
    )]
    ObjectPermission,
    #[sea_orm(has_many = "super::lite_llm_verified_subject::Entity")]
    VerifiedSubjects,
    #[sea_orm(has_many = "super::lite_llm_organization_membership::Entity")]
    OrganizationMemberships,
    #[sea_orm(
        has_many = "super::lite_llm_invitation_link::Entity",
        from = "Column::UserId",
        to = "super::lite_llm_invitation_link::Column::CreatedBy"
    )]
    InvitationsCreated,
    #[sea_orm(
        has_many = "super::lite_llm_invitation_link::Entity",
        from = "Column::UserId",
        to = "super::lite_llm_invitation_link::Column::UpdatedBy"
    )]
    InvitationsUpdated,
    #[sea_orm(
        has_many = "super::lite_llm_invitation_link::Entity",
        from = "Column::UserId",
        to = "super::lite_llm_invitation_link::Column::UserId"
    )]
    InvitationsUser,
}

impl Related<super::lite_llm_organization_table::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::LitellmOrganizationTable.def()
    }
}

impl Related<super::lite_llm_object_permission_table::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ObjectPermission.def()
    }
}

impl Related<super::lite_llm_verified_subject::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::VerifiedSubjects.def()
    }
}

impl Related<super::lite_llm_organization_membership::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::OrganizationMemberships.def()
    }
}

impl Related<super::lite_llm_invitation_link::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::InvitationsCreated.def()
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
            self.created_at = Set(Some(now.naive_utc()));
        }
        if !self.updated_at.is_set() {
            self.updated_at = Set(Some(now.naive_utc()));
        }
        Ok(self)
    }
}
