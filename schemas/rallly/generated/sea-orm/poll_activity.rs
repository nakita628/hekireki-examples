use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "poll_activities")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub poll_id: String,
    pub r#type: String,
    pub user_id: Option<String>,
    pub participant_id: Option<String>,
    pub invite_id: Option<String>,
    pub option_id: Option<String>,
    #[sea_orm(default_value = "{}")]
    pub payload: Json,
    pub created_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::poll::Entity",
        from = "Column::PollId",
        to = "super::poll::Column::Id",
        on_delete = "Cascade"
    )]
    Poll,
}

impl Related<super::poll::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Poll.def()
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
