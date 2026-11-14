use super::invite_status::INVITE_STATUS;
use super::organization_user_role::OrganizationUserRole;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "OrganizationInvite")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub email: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "inviteCode")]
    pub invite_code: String,
    pub expiration: Option<DateTime>,
    #[sea_orm(default_value = "PENDING")]
    pub status: INVITE_STATUS,
    #[sea_orm(column_name = "organizationId")]
    pub organization_id: String,
    #[sea_orm(column_name = "teamIds")]
    pub team_ids: String,
    #[sea_orm(column_name = "teamAssignments")]
    pub team_assignments: Option<Json>,
    pub role: OrganizationUserRole,
    #[sea_orm(column_name = "requestedBy")]
    pub requested_by: Option<String>,
    #[sea_orm(column_name = "subscriptionId")]
    pub subscription_id: Option<String>,
    #[sea_orm(column_name = "acceptedByUserId")]
    pub accepted_by_user_id: Option<String>,
    #[sea_orm(column_name = "acceptedViaIdentifierId")]
    pub accepted_via_identifier_id: Option<String>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::organization::Entity",
        from = "Column::OrganizationId",
        to = "super::organization::Column::Id"
    )]
    Organization,
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::RequestedBy",
        to = "super::user::Column::Id"
    )]
    RequestedByUser,
}

impl Related<super::organization::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Organization.def()
    }
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::RequestedByUser.def()
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
