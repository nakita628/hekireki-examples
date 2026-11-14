class BatchEvaluation < ApplicationRecord
  self.table_name = "BatchEvaluation"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  attribute :data, default: -> { JSON.parse("{}") }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :id, uniqueness: true
  validates :status, presence: true
  validates :score, presence: true
  validates :passed, inclusion: { in: [ true, false ] }
  validates :details, presence: true
  validates :cost, presence: true
  validates :datasetSlug, presence: true
  validates :evaluation, presence: true

  belongs_to :experiment, foreign_key: "experimentId", inverse_of: :batch_evaluations
  belongs_to :project, foreign_key: "projectId", inverse_of: :batch_evaluations
  belongs_to :dataset, foreign_key: "datasetId", inverse_of: :batch_evaluations
end
