class Scenario < ApplicationRecord
  self.table_name = "Scenario"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  attribute :criteria, default: -> { [] }
  attribute :labels, default: -> { [] }
  attribute :version, default: 1
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true
  validates :situation, presence: true

  belongs_to :project, foreign_key: "projectId", inverse_of: :scenarios
  belongs_to :test_suite, class_name: "SimulationSuite", foreign_key: "testSuiteId", optional: true,
                          inverse_of: :test_suite_scenarios
  belongs_to :last_updated_by, class_name: "User", foreign_key: "lastUpdatedById", optional: true,
                               inverse_of: :scenarios
  has_many :versions, class_name: "ScenarioVersion", foreign_key: "scenarioId", inverse_of: :scenario,
                      dependent: :destroy
end
