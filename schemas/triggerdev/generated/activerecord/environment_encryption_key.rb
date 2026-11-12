class EnvironmentEncryptionKey < ApplicationRecord
  self.table_name = "EnvironmentEncryptionKey"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :revision, default: 0
  alias_attribute :created_at, :createdAt

  enum :wrappingProvider, { platform_keyring: "PLATFORM_KEYRING", aws_kms: "AWS_KMS" }, validate: true

  validates :id, uniqueness: { scope: :environmentId }
  validates :wrappedKey, exclusion: { in: [ nil ] }
  validates :wrappingKeyReference, presence: true

  belongs_to :environment, class_name: "RuntimeEnvironment", foreign_key: "environmentId", inverse_of: :encryption_keys
  has_many :active_for, class_name: "EnvironmentEncryptionPolicy", foreign_key: [ :activeKeyId, :environmentId ],
                        primary_key: [ :id, :environmentId ], inverse_of: :active_key, dependent: :restrict_with_error
end
