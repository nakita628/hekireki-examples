class YearInReview < ApplicationRecord
  self.table_name = "YearInReview"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :status, default: "pending"
  attribute :attempts, default: 0
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :teamId, presence: true
  validates :stats, exclusion: { in: [ nil ] }
end
