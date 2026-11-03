use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "ClaimableCustomPlan")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "claimedAt")]
    pub claimed_at: Option<DateTime>,
    pub name: String,
    pub description: Option<String>,
    pub price: i32,
    pub currency: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "workspaceId")]
    pub workspace_id: String,
    #[sea_orm(column_name = "chatsLimit")]
    pub chats_limit: i32,
    #[sea_orm(column_name = "storageLimit")]
    pub storage_limit: i32,
    #[sea_orm(column_name = "seatsLimit")]
    pub seats_limit: i32,
    #[sea_orm(column_name = "isYearly", default_value = false)]
    pub is_yearly: bool,
    #[sea_orm(column_name = "companyName")]
    pub company_name: Option<String>,
    #[sea_orm(column_name = "vatType")]
    pub vat_type: Option<String>,
    #[sea_orm(column_name = "vatValue")]
    pub vat_value: Option<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::workspace::Entity",
        from = "Column::WorkspaceId",
        to = "super::workspace::Column::Id",
        on_delete = "Cascade"
    )]
    Workspace,
}

impl Related<super::workspace::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Workspace.def()
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
        Ok(self)
    }
}
