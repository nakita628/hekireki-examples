class UsersAndCollections < ApplicationRecord
  self.table_name = "UsersAndCollections"
  self.primary_key = %w[userId collectionId]

  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :canCreate, inclusion: { in: [ true, false ] }
  validates :canUpdate, inclusion: { in: [ true, false ] }
  validates :canDelete, inclusion: { in: [ true, false ] }

  belongs_to :user, foreign_key: "userId", inverse_of: :collections_joined
  belongs_to :collection, foreign_key: "collectionId", inverse_of: :members
end
