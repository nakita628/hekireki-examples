use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "TeamRequest")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "collectionID")]
    pub collection_id: String,
    #[sea_orm(column_name = "teamID")]
    pub team_id: String,
    pub title: String,
    pub request: Json,
    #[sea_orm(column_name = "mockExamples")]
    pub mock_examples: Option<Json>,
    #[sea_orm(column_name = "orderIndex")]
    pub order_index: i32,
    #[sea_orm(column_name = "createdOn", column_type = "TimestampWithTimeZone")]
    pub created_on: DateTimeWithTimeZone,
    #[sea_orm(column_name = "updatedOn", column_type = "TimestampWithTimeZone")]
    pub updated_on: DateTimeWithTimeZone,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::team_collection::Entity",
        from = "Column::CollectionID",
        to = "super::team_collection::Column::Id",
        on_delete = "Cascade"
    )]
    Collection,
    #[sea_orm(
        belongs_to = "super::team::Entity",
        from = "Column::TeamID",
        to = "super::team::Column::Id",
        on_delete = "Cascade"
    )]
    Team,
}

impl Related<super::team_collection::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Collection.def()
    }
}

impl Related<super::team::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Team.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.created_on.is_not_set() {
            self.created_on = Set(now.fixed_offset());
        }
        if !self.updated_on.is_set() {
            self.updated_on = Set(now.fixed_offset());
        }
        Ok(self)
    }
}
