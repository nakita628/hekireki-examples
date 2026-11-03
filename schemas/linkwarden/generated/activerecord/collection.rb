class Collection < ApplicationRecord
  self.table_name = "Collection"

  attribute :description, default: ""
  attribute :color, default: "\#0ea5e9"
  attribute :isPublic, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true

  belongs_to :parent, class_name: "Collection", foreign_key: "parentId", optional: true, inverse_of: :sub_collections
  belongs_to :owner, class_name: "User", foreign_key: "ownerId", inverse_of: :collections
  belongs_to :created_by, class_name: "User", foreign_key: "createdById", optional: true,
                          inverse_of: :created_collections
  has_many :sub_collections, class_name: "Collection", foreign_key: "parentId", inverse_of: :parent, dependent: :destroy
  has_many :members, class_name: "UsersAndCollections", foreign_key: "collectionId", inverse_of: :collection,
                     dependent: :destroy
  has_many :links, foreign_key: "collectionId", inverse_of: :collection, dependent: :destroy
  has_many :rss_subscriptions, foreign_key: "collectionId", inverse_of: :collection, dependent: :destroy
  has_many :dashboard_section, class_name: "DashboardSection", foreign_key: "collectionId", inverse_of: :collection,
                               dependent: :destroy
end
