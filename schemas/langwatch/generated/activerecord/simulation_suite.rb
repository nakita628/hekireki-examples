class SimulationSuite < ApplicationRecord
  self.table_name = "SimulationSuite"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  attribute :scenarioIds, default: -> { [] }
  attribute :repeatCount, default: 1
  attribute :labels, default: -> { [] }
  attribute :kind, default: "run_plan"
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true
  validates :slug, presence: true, uniqueness: { scope: :projectId }
  validates :targets, exclusion: { in: [ nil ] }

  belongs_to :project, foreign_key: "projectId", inverse_of: :suite_configurations
  has_many :test_suite_scenarios, class_name: "Scenario", foreign_key: "testSuiteId", inverse_of: :test_suite,
                                  dependent: :nullify
end
