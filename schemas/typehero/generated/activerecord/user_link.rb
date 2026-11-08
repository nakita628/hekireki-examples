class UserLink < ApplicationRecord
  self.table_name = "UserLink"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :url, presence: true, length: { maximum: 256 }

  has_and_belongs_to_many :user, join_table: "_UserToUserLink", foreign_key: "B", association_foreign_key: "A"
end
