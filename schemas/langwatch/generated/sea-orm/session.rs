use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Session")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "sessionToken")]
    pub session_token: String,
    #[sea_orm(column_name = "userId")]
    pub user_id: String,
    pub expires: DateTime,
    #[sea_orm(column_name = "ipAddress")]
    pub ip_address: Option<String>,
    #[sea_orm(column_name = "userAgent", column_type = "Text")]
    pub user_agent: Option<String>,
    #[sea_orm(column_name = "identifierId")]
    pub identifier_id: Option<String>,
    pub amr: Vec<String>,
    #[sea_orm(column_name = "startedBySignUp", default_value = false)]
    pub started_by_sign_up: bool,
    pub impersonating: Option<Json>,
    #[sea_orm(column_name = "actorUserId")]
    pub actor_user_id: Option<String>,
    #[sea_orm(column_name = "subjectUserId")]
    pub subject_user_id: Option<String>,
    #[sea_orm(column_name = "impersonationReason", column_type = "Text")]
    pub impersonation_reason: Option<String>,
    #[sea_orm(column_name = "impersonationExpiresAt")]
    pub impersonation_expires_at: Option<DateTime>,
    #[sea_orm(column_name = "lastSeenAt")]
    pub last_seen_at: Option<DateTime>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::UserId",
        to = "super::user::Column::Id",
        on_delete = "Cascade"
    )]
    User,
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::User.def()
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
