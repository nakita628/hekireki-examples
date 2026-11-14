class BugReport < ApplicationRecord
  self.table_name = "BugReport"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  attribute :sessionTruncated, default: false
  alias_attribute :created_at, :createdAt

  validates :source, presence: true
  validates :kind, presence: true
  validates :title, presence: true
end
