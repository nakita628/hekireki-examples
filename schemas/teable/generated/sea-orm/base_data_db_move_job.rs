use super::base_data_db_move_job_state::BaseDataDbMoveJobState;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "base_data_db_move_job")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub base_id: String,
    pub source_space_id: String,
    pub target_space_id: String,
    pub source_connection_id: Option<String>,
    pub target_connection_id: Option<String>,
    #[sea_orm(default_value = "pending")]
    pub state: BaseDataDbMoveJobState,
    pub inventory: Option<Json>,
    pub copy_stats: Option<Json>,
    pub validation_stats: Option<Json>,
    pub last_error: Option<String>,
    pub started_at: Option<DateTime>,
    pub completed_at: Option<DateTime>,
    pub created_by: String,
    pub created_time: DateTime,
    pub last_modified_time: Option<DateTime>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::base::Entity",
        from = "Column::BaseId",
        to = "super::base::Column::Id",
        on_delete = "Cascade"
    )]
    Base,
    #[sea_orm(
        belongs_to = "super::space::Entity",
        from = "Column::SourceSpaceId",
        to = "super::space::Column::Id",
        on_delete = "Cascade"
    )]
    SourceSpace,
    #[sea_orm(
        belongs_to = "super::space::Entity",
        from = "Column::TargetSpaceId",
        to = "super::space::Column::Id",
        on_delete = "Cascade"
    )]
    TargetSpace,
}

impl Related<super::base::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Base.def()
    }
}

impl Related<super::space::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::SourceSpace.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.created_time.is_not_set() {
            self.created_time = Set(now.naive_utc());
        }
        if !self.last_modified_time.is_set() {
            self.last_modified_time = Set(Some(now.naive_utc()));
        }
        Ok(self)
    }
}
