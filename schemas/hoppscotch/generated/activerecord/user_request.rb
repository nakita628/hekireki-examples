class UserRequest < ApplicationRecord
  self.table_name = "UserRequest"
  self.inheritance_column = nil

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :updated_at, :updatedOn

  enum :type, { rest: "REST", gql: "GQL" }, validate: true

  validates :title, presence: true
  validates :request, exclusion: { in: [ nil ] }
  validates :orderIndex, presence: true, uniqueness: { scope: [ :userUid, :collectionID ] }

  belongs_to :user_collection, foreign_key: "collectionID", inverse_of: :requests
  belongs_to :user, foreign_key: "userUid", inverse_of: :user_requests
end
