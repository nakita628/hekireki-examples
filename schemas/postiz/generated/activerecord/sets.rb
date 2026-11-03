class Sets < ApplicationRecord
  self.table_name = "Sets"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true
  validates :content, presence: true

  belongs_to :organization, foreign_key: "organizationId", inverse_of: :sets
end
