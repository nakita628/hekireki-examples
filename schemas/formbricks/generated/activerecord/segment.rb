class Segment < ApplicationRecord
  self.table_name = "Segment"
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }
  attribute :isPrivate, default: true
  attribute :filters, default: -> { JSON.parse("[]") }

  validates :title, presence: true, uniqueness: { scope: :workspaceId }

  belongs_to :workspace, foreign_key: "workspaceId", inverse_of: :segments
  has_many :surveys, foreign_key: "segmentId", inverse_of: :segment, dependent: :nullify
end
