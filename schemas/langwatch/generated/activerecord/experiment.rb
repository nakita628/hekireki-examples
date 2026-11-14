class Experiment < ApplicationRecord
  self.table_name = "Experiment"
  self.inheritance_column = nil
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  attribute :workbenchVersion, default: 0
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :type, { dspy: "DSPY", batch_evaluation: "BATCH_EVALUATION", batch_evaluation_v2: "BATCH_EVALUATION_V2", evaluations_v3: "EVALUATIONS_V3" },
       validate: true

  validates :slug, presence: true, uniqueness: { scope: :projectId }

  belongs_to :project, foreign_key: "projectId", inverse_of: :experiments
  belongs_to :workflow, foreign_key: "workflowId", optional: true, inverse_of: :experiments
  has_one :monitor, class_name: "MonitorRecord", foreign_key: "experimentId", inverse_of: :experiment,
                    dependent: :restrict_with_error
  has_many :batch_evaluations, foreign_key: "experimentId", inverse_of: :experiment, dependent: :restrict_with_error
end
