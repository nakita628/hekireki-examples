class EnvironmentVariableValue < ApplicationRecord
  self.table_name = "EnvironmentVariableValue"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :isSecret, default: false
  attribute :version, default: 1
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :variableId, uniqueness: { scope: :environmentId }

  belongs_to :value_reference, class_name: "SecretReference", foreign_key: "valueReferenceId", optional: true,
                               inverse_of: :environment_variable_values
  belongs_to :variable, class_name: "EnvironmentVariable", foreign_key: "variableId", inverse_of: :values
  belongs_to :environment, class_name: "RuntimeEnvironment", foreign_key: "environmentId",
                           inverse_of: :environment_variable_values
end
