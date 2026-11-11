class LiteLLMUserTable < ApplicationRecord
  self.table_name = "LiteLLM_UserTable"
  self.primary_key = "user_id"

  attribute :teams, default: -> { [] }
  attribute :spend, default: 0
  attribute :models, default: -> { [] }
  attribute :metadata, default: -> { JSON.parse("{}") }
  attribute :allowed_cache_controls, default: -> { [] }
  attribute :policies, default: -> { [] }
  attribute :model_spend, default: -> { JSON.parse("{}") }
  attribute :model_max_budget, default: -> { JSON.parse("{}") }

  validates :sso_user_id, uniqueness: { allow_nil: true }

  belongs_to :litellm_organization_table, class_name: "LiteLLMOrganizationTable", foreign_key: "organization_id",
                                          optional: true, inverse_of: :users
  belongs_to :object_permission, class_name: "LiteLLMObjectPermissionTable", optional: true, inverse_of: :users
  has_many :verified_subjects, class_name: "LiteLLMVerifiedSubject", foreign_key: "user_id", inverse_of: :user,
                               dependent: :destroy
  has_many :organization_memberships, class_name: "LiteLLMOrganizationMembership", foreign_key: "user_id",
                                      inverse_of: :user, dependent: :restrict_with_error
  has_many :invitations_created, class_name: "LiteLLMInvitationLink", foreign_key: "created_by",
                                 inverse_of: :lite_llm_user_table_created, dependent: :restrict_with_error
  has_many :invitations_updated, class_name: "LiteLLMInvitationLink", foreign_key: "updated_by",
                                 inverse_of: :lite_llm_user_table_updated, dependent: :restrict_with_error
  has_many :invitations_user, class_name: "LiteLLMInvitationLink", foreign_key: "user_id",
                              inverse_of: :lite_llm_user_table_user, dependent: :restrict_with_error
end
