class Users < ApplicationRecord
  attribute :role, default: "default"
  attribute :suspended, default: 0
  attribute :seen_recovery_codes, default: false
  attribute :createdAt, PrismaDateTime.new, default: -> { Time.current }
  attribute :lastUpdatedAt, PrismaDateTime.new, default: -> { Time.current }
  attribute :bio, default: ""
  alias_attribute :created_at, :createdAt

  validates :username, uniqueness: { allow_nil: true }
  validates :password, presence: true

  has_many :workspace_chats, class_name: "WorkspaceChats", foreign_key: "user_id", inverse_of: :users,
                             dependent: :destroy
  has_many :workspace_users, class_name: "WorkspaceUsers", foreign_key: "user_id", inverse_of: :users,
                             dependent: :destroy
  has_many :embed_configs, class_name: "EmbedConfigs", foreign_key: "usersId", inverse_of: :users, dependent: :nullify
  has_many :embed_chats, class_name: "EmbedChats", foreign_key: "usersId", inverse_of: :users, dependent: :nullify
  has_many :threads, class_name: "WorkspaceThreads", foreign_key: "user_id", inverse_of: :user, dependent: :destroy
  has_many :recovery_codes, class_name: "RecoveryCodes", foreign_key: "user_id", inverse_of: :user, dependent: :destroy
  has_many :password_reset_tokens, class_name: "PasswordResetTokens", foreign_key: "user_id", inverse_of: :user,
                                   dependent: :destroy
  has_many :workspace_agent_invocations, class_name: "WorkspaceAgentInvocations", foreign_key: "user_id",
                                         inverse_of: :user, dependent: :destroy
  has_many :slash_command_presets, class_name: "SlashCommandPresets", foreign_key: "userId", inverse_of: :user,
                                   dependent: :destroy
  has_many :browser_extension_api_keys, class_name: "BrowserExtensionApiKeys", foreign_key: "user_id", inverse_of: :user,
                                        dependent: :destroy
  has_many :temporary_auth_tokens, class_name: "TemporaryAuthTokens", foreign_key: "userId", inverse_of: :user,
                                   dependent: :destroy
  has_many :system_prompt_variables, class_name: "SystemPromptVariables", foreign_key: "userId", inverse_of: :user,
                                     dependent: :destroy
  has_many :prompt_history, foreign_key: "modifiedBy", inverse_of: :user, dependent: :nullify
  has_many :desktop_mobile_devices, class_name: "DesktopMobileDevices", foreign_key: "userId", inverse_of: :user,
                                    dependent: :destroy
  has_many :workspace_parsed_files, class_name: "WorkspaceParsedFiles", foreign_key: "userId", inverse_of: :user,
                                    dependent: :destroy
  has_many :memories, class_name: "Memories", foreign_key: "user_id", inverse_of: :user, dependent: :destroy
end
