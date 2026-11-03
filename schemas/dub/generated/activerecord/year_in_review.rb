class YearInReview < ApplicationRecord
  self.table_name = "YearInReview"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt

  validates :year, presence: true
  validates :totalLinks, presence: true
  validates :totalClicks, presence: true
  validates :topLinks, exclusion: { in: [ nil ] }
  validates :topCountries, exclusion: { in: [ nil ] }

  belongs_to :workspace, class_name: "Project", foreign_key: "workspaceId", inverse_of: :year_in_reviews
end
