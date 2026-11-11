class LiteLLMInvitationLink < ApplicationRecord
  self.table_name = "LiteLLM_InvitationLink"
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { SecureRandom.uuid }
  attribute :is_accepted, default: false

  validates :expires_at, presence: true

  belongs_to :lite_llm_user_table_user, class_name: "LiteLLMUserTable", foreign_key: "user_id",
                                        inverse_of: :invitations_user
  belongs_to :lite_llm_user_table_created, class_name: "LiteLLMUserTable", foreign_key: "created_by",
                                           inverse_of: :invitations_created
  belongs_to :lite_llm_user_table_updated, class_name: "LiteLLMUserTable", foreign_key: "updated_by",
                                           inverse_of: :invitations_updated
end
