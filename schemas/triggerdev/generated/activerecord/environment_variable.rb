class EnvironmentVariable < ApplicationRecord
  self.table_name = "EnvironmentVariable"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :friendlyId, presence: true, uniqueness: true
  validates :key, presence: true, uniqueness: { scope: :projectId }

  belongs_to :project, foreign_key: "projectId", inverse_of: :environment_variables
  has_many :values, class_name: "EnvironmentVariableValue", foreign_key: "variableId", inverse_of: :variable,
                    dependent: :destroy
end
