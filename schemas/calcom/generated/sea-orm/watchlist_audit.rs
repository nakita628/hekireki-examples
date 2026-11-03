use super::watchlist_action::WatchlistAction;
use super::watchlist_type::WatchlistType;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "WatchlistAudit")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_type = "Uuid")]
    pub id: String,
    pub r#type: WatchlistType,
    pub value: String,
    pub description: Option<String>,
    #[sea_orm(default_value = "REPORT")]
    pub action: WatchlistAction,
    #[sea_orm(column_name = "changedAt")]
    pub changed_at: DateTime,
    #[sea_orm(column_name = "changedByUserId")]
    pub changed_by_user_id: Option<i32>,
    #[sea_orm(column_name = "watchlistId", column_type = "Uuid")]
    pub watchlist_id: Option<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::watchlist::Entity",
        from = "Column::WatchlistId",
        to = "super::watchlist::Column::Id",
        on_delete = "SetNull"
    )]
    Watchlist,
}

impl Related<super::watchlist::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Watchlist.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    fn new() -> Self {
        Self {
            id: Set(uuid::Uuid::now_v7().to_string()),
            ..ActiveModelTrait::default()
        }
    }

    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.changed_at.is_not_set() {
            self.changed_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
