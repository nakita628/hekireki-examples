class EmbedConfigs < ApplicationRecord
  attribute :enabled, default: false
  attribute :chat_mode, default: "query"
  attribute :allow_model_override, default: false
  attribute :allow_temperature_override, default: false
  attribute :allow_prompt_override, default: false
  attribute :message_limit, default: 20
  attribute :createdAt, PrismaDateTime.new, default: -> { Time.current }
  alias_attribute :created_at, :createdAt

  validates :uuid, presence: true, uniqueness: true

  belongs_to :workspace, class_name: "Workspaces", inverse_of: :embed_configs
  belongs_to :users, foreign_key: "usersId", optional: true, inverse_of: :embed_configs
  has_many :embed_chats, class_name: "EmbedChats", foreign_key: "embed_id", inverse_of: :embed_config,
                         dependent: :destroy
end
