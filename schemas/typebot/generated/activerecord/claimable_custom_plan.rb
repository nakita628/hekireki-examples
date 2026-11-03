class ClaimableCustomPlan < ApplicationRecord
  self.table_name = "ClaimableCustomPlan"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :isYearly, default: false
  alias_attribute :created_at, :createdAt

  validates :name, presence: true
  validates :price, presence: true
  validates :currency, presence: true
  validates :workspaceId, uniqueness: true
  validates :chatsLimit, presence: true
  validates :storageLimit, presence: true
  validates :seatsLimit, presence: true

  belongs_to :workspace, foreign_key: "workspaceId", inverse_of: :claimable_custom_plan
end
