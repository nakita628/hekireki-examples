class Workspace < ApplicationRecord
  self.table_name = "Workspace"
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }
  attribute :styling, default: -> { JSON.parse("{\"allowStyleOverwrite\":true}") }
  attribute :config, default: -> { JSON.parse("{}") }
  attribute :recontactDays, default: 7
  attribute :linkSurveyBranding, default: true
  attribute :inAppSurveyBranding, default: true
  attribute :clickOutsideClose, default: true
  attribute :appSetupCompleted, default: false

  enum :placement, { bottom_left: "bottomLeft", bottom_right: "bottomRight", top_left: "topLeft", top_right: "topRight", center: "center" },
       default: :bottom_right, validate: true
  enum :overlay, { none: "none", light: "light", dark: "dark" }, default: :none, prefix: true, validate: true

  validates :name, presence: true, uniqueness: { scope: :organizationId }
  validates :legacyEnvironmentId, uniqueness: { allow_nil: true }

  belongs_to :organization, foreign_key: "organizationId", inverse_of: :workspaces
  has_many :languages, foreign_key: "workspaceId", inverse_of: :workspace, dependent: :destroy
  has_many :embedded_data, class_name: "EmbeddedData", foreign_key: "workspaceId", inverse_of: :workspace,
                           dependent: :destroy
  has_many :workspace_teams, foreign_key: "workspaceId", inverse_of: :workspace, dependent: :destroy
  has_many :feedback_directory_workspaces, foreign_key: "workspaceId", inverse_of: :workspace, dependent: :destroy
  has_many :charts, foreign_key: "workspaceId", inverse_of: :workspace, dependent: :destroy
  has_many :dashboards, foreign_key: "workspaceId", inverse_of: :workspace, dependent: :destroy
  has_many :creatives, foreign_key: "workspaceId", inverse_of: :workspace, dependent: :destroy
  has_many :attachment_exports, foreign_key: "workspaceId", inverse_of: :workspace, dependent: :destroy
  has_many :surveys, foreign_key: "workspaceId", inverse_of: :workspace, dependent: :destroy
  has_many :contacts, foreign_key: "workspaceId", inverse_of: :workspace, dependent: :destroy
  has_many :action_classes, foreign_key: "workspaceId", inverse_of: :workspace, dependent: :destroy
  has_many :contact_attribute_keys, foreign_key: "workspaceId", inverse_of: :workspace, dependent: :destroy
  has_many :webhooks, foreign_key: "workspaceId", inverse_of: :workspace, dependent: :destroy
  has_many :tags, foreign_key: "workspaceId", inverse_of: :workspace, dependent: :destroy
  has_many :segments, foreign_key: "workspaceId", inverse_of: :workspace, dependent: :destroy
  has_many :integrations, foreign_key: "workspaceId", inverse_of: :workspace, dependent: :destroy
  has_many :api_key_workspaces, foreign_key: "workspaceId", inverse_of: :workspace, dependent: :destroy
  has_many :feedback_sources, foreign_key: "workspaceId", inverse_of: :workspace, dependent: :destroy
  has_many :workflows, foreign_key: "workspaceId", inverse_of: :workspace, dependent: :destroy
  has_many :workflow_runs, foreign_key: "workspaceId", inverse_of: :workspace, dependent: :destroy
end
