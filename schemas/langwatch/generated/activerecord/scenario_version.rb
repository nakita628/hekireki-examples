class ScenarioVersion < ApplicationRecord
  self.table_name = "ScenarioVersion"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  attribute :schemaVersion, default: 1
  alias_attribute :created_at, :createdAt

  validates :projectId, presence: true
  validates :version, presence: true, uniqueness: { scope: :scenarioId }
  validates :authorLabel, presence: true
  validates :snapshot, exclusion: { in: [ nil ] }

  belongs_to :scenario, foreign_key: "scenarioId", inverse_of: :versions
end
