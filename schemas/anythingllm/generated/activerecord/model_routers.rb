class ModelRouters < ApplicationRecord
  attribute :cooldown_seconds, default: 30
  attribute :createdAt, PrismaDateTime.new, default: -> { Time.current }
  attribute :lastUpdatedAt, PrismaDateTime.new, default: -> { Time.current }
  alias_attribute :created_at, :createdAt

  validates :name, presence: true, uniqueness: true
  validates :fallback_provider, presence: true
  validates :fallback_model, presence: true

  has_many :rules, class_name: "ModelRouterRules", foreign_key: "router_id", inverse_of: :router, dependent: :destroy
end
