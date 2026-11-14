class ExperimentVersion < ApplicationRecord
  self.table_name = "ExperimentVersion"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  attribute :autoSaved, default: false
  attribute :schemaVersion, default: "1"
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :experimentId, presence: true, uniqueness: { scope: :version }
  validates :projectId, presence: true
  validates :version, presence: true
  validates :counterVersion, presence: true
  validates :authorLabel, presence: true
  validates :state, exclusion: { in: [ nil ] }
end
