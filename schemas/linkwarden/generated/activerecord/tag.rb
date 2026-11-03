class Tag < ApplicationRecord
  self.table_name = "Tag"

  attribute :aiGenerated, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true, uniqueness: { scope: :ownerId }

  belongs_to :owner, class_name: "User", foreign_key: "ownerId", inverse_of: :tags
  has_and_belongs_to_many :links, join_table: "_LinkToTag", foreign_key: "B", association_foreign_key: "A"
end
