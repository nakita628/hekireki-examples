class ApiKey < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }
  attribute :is_in_app_agent_key, default: false

  enum :scope, { organization: "ORGANIZATION", project: "PROJECT", user: "USER" }, default: :project, validate: true

  validates :public_key, presence: true, uniqueness: true
  validates :hashed_secret_key, presence: true, uniqueness: true
  validates :fast_hashed_secret_key, uniqueness: { allow_nil: true }
  validates :display_secret_key, presence: true

  belongs_to :project, optional: true, inverse_of: :api_keys
  belongs_to :organization, optional: true, inverse_of: :api_key
  belongs_to :created_by_user, class_name: "User", optional: true, inverse_of: :api_keys_created
  belongs_to :user, optional: true, inverse_of: :api_keys
  belongs_to :created_by_api_key, class_name: "ApiKey", optional: true, inverse_of: :api_keys_created
  has_one :gateway_association, class_name: "GatewayApiKeyAssociation", inverse_of: :api_key, dependent: :destroy
  has_many :api_keys_created, class_name: "ApiKey", foreign_key: "created_by_api_key_id",
                              inverse_of: :created_by_api_key, dependent: :nullify
  has_many :role_assignments, dependent: :destroy
end
