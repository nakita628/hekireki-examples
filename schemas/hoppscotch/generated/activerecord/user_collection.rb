class UserCollection < ApplicationRecord
  self.table_name = "UserCollection"
  self.inheritance_column = nil

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :updated_at, :updatedOn

  enum :type, { rest: "REST", gql: "GQL" }, validate: true

  validates :title, presence: true
  validates :orderIndex, presence: true,
                         uniqueness: { scope: [ :userUid, :parentID ], conditions: -> { where.not(parentID: nil) } }

  belongs_to :parent, class_name: "UserCollection", foreign_key: "parentID", optional: true, inverse_of: :children
  belongs_to :user, foreign_key: "userUid", inverse_of: :user_collections
  has_many :children, class_name: "UserCollection", foreign_key: "parentID", inverse_of: :parent, dependent: :destroy
  has_many :requests, class_name: "UserRequest", foreign_key: "collectionID", inverse_of: :user_collection,
                      dependent: :destroy
end
