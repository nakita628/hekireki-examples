class EnvironmentEncryptionPolicy < ApplicationRecord
  self.table_name = "EnvironmentEncryptionPolicy"
  self.primary_key = "environmentId"

  attribute :revision, default: 0

  enum :wrappingProvider, { platform_keyring: "PLATFORM_KEYRING", aws_kms: "AWS_KMS" }, validate: { allow_nil: true }

  belongs_to :environment, class_name: "RuntimeEnvironment", foreign_key: "environmentId",
                           inverse_of: :encryption_policy
  belongs_to :active_key, class_name: "EnvironmentEncryptionKey", foreign_key: [ :activeKeyId, :environmentId ],
                          primary_key: [ :id, :environmentId ], optional: true, inverse_of: :active_for
end
