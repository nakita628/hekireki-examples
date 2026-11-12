class Tag < ApplicationRecord
  self.table_name = "tag"

  attribute :name, default: ""
  attribute :icon, default: ""
  attribute :parent, default: 0
  attribute :sortOrder, default: 0
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  belongs_to :account, class_name: "Accounts", foreign_key: "accountId", optional: true, inverse_of: :tags
  has_many :tags_to_note, foreign_key: "tagId", inverse_of: :tag, dependent: :restrict_with_error
end
