class ProjectInsight < ApplicationRecord
  attribute :displayName, default: ""
  attribute :payload, default: -> { JSON.parse("{}") }
  attribute :impactScore, default: 0
  attribute :emailWorthy, default: false
  attribute :referenceWorthy, default: false
  attribute :version, default: 1
  alias_attribute :updated_at, :lastUpdatedAt

  enum :state, { active: "active", suppressed: "suppressed", closed: "closed" }, default: :active, validate: true

  validates :projectId, presence: true, uniqueness: { scope: [ :moduleKey, :dimensionKey, :windowKind, :state ] }
  validates :moduleKey, presence: true
  validates :dimensionKey, presence: true
  validates :windowKind, presence: true
  validates :title, presence: true

  has_many :events, class_name: "InsightEvent", foreign_key: "insightId", inverse_of: :insight, dependent: :destroy
end
