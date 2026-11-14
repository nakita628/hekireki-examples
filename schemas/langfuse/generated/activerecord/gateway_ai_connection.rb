class GatewayAiConnection < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }
  attribute :custom_models, default: -> { JSON.parse("[]") }
  attribute :extra_header_keys, default: -> { [] }

  enum :provider, { openai: "openai", anthropic: "anthropic", bedrock: "bedrock", openrouter: "openrouter", xai: "xai", vercel: "vercel", azure: "azure", google_ai_studio: "google_ai_studio", vertex: "vertex" },
       validate: true
  enum :status, { enabled: "enabled", disabled: "disabled", error: "error" }, default: :enabled, validate: true

  validates :name, presence: true, uniqueness: { scope: :organization_id }
  validates :encrypted_credentials, presence: true
  validates :display_secret_key, presence: true

  belongs_to :organization, inverse_of: :gateway_ai_connections
end
