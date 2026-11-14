class GatewayBudgetBucketBoundary < ApplicationRecord
  self.table_name = "GatewayBudgetBucketBoundary"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :organizationId, presence: true
  validates :bucketScopeId, presence: true, uniqueness: { scope: :budgetId }
  validates :periodStartedAt, presence: true

  belongs_to :budget, class_name: "GatewayBudget", foreign_key: "budgetId", inverse_of: :bucket_boundaries
end
