class Customer < ApplicationRecord
  self.table_name = "Customer"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true,
                   uniqueness: { scope: [ :orgId, :deletedAt ], conditions: -> { where.not(deletedAt: nil) } }

  belongs_to :organization, foreign_key: "orgId", inverse_of: :customers
  has_many :integrations, foreign_key: "customerId", inverse_of: :customer, dependent: :nullify
end
