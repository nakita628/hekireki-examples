use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "SeatRequest")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "organizationId")]
    pub organization_id: String,
    pub email: String,
    #[sea_orm(column_name = "userId")]
    pub user_id: Option<String>,
    pub name: Option<String>,
    pub status: String,
    pub source: String,
    #[sea_orm(column_name = "idpRefused", default_value = false)]
    pub idp_refused: bool,
    #[sea_orm(column_name = "idpKind")]
    pub idp_kind: Option<String>,
    #[sea_orm(column_name = "salesNotifiedAt")]
    pub sales_notified_at: Option<DateTime>,
    #[sea_orm(column_name = "openedAt")]
    pub opened_at: DateTime,
    #[sea_orm(column_name = "resolvedAt")]
    pub resolved_at: Option<DateTime>,
    #[sea_orm(column_name = "resolvedByUserId")]
    pub resolved_by_user_id: Option<String>,
    #[sea_orm(column_name = "seatPendingSince")]
    pub seat_pending_since: Option<DateTime>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {}

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
