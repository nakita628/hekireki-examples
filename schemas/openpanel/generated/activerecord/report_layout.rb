class ReportLayout < ApplicationRecord
  attribute :gridColumns, default: 12
  attribute :x, default: 0
  attribute :y, default: 0
  attribute :w, default: 8
  attribute :h, default: 6
  attribute :minW, default: 4
  attribute :minH, default: 4
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :reportId, uniqueness: true

  belongs_to :report, foreign_key: "reportId", inverse_of: :layout
end
