class SecretReference < ApplicationRecord
  self.table_name = "SecretReference"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :provider, { database: "DATABASE", aws_param_store: "AWS_PARAM_STORE" }, default: :database, validate: true

  validates :key, presence: true, uniqueness: true

  has_many :environment_variable_values, foreign_key: "valueReferenceId", inverse_of: :value_reference,
                                         dependent: :nullify
  has_many :organization_integration, class_name: "OrganizationIntegration", foreign_key: "tokenReferenceId",
                                      inverse_of: :token_reference, dependent: :destroy
  has_many :user, class_name: "User", foreign_key: "mfaSecretReferenceId", inverse_of: :mfa_secret_reference,
                  dependent: :nullify
end
