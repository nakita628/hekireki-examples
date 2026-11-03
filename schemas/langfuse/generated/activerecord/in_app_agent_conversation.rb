class InAppAgentConversation < ApplicationRecord
  self.primary_key = %w[id project_id]

  attribute :always_allowed_tools, default: -> { [] }

  enum :visibility_scope, { personal: "PERSONAL", project: "PROJECT" }, default: :personal, validate: true

  belongs_to :project, inverse_of: :in_app_agent_conversation
  belongs_to :created_by_user, class_name: "User", optional: true, inverse_of: :in_app_agent_conversations
  has_many :events, class_name: "InAppAgentEvent", foreign_key: %i[conversation_id project_id],
                    inverse_of: :conversation, dependent: :destroy
  has_many :runs, class_name: "InAppAgentRun", foreign_key: %i[conversation_id project_id], inverse_of: :conversation,
                  dependent: :destroy
  has_many :pending_tool_approvals, class_name: "InAppAgentPendingToolApproval",
                                    foreign_key: %i[conversation_id project_id], inverse_of: :conversation,
                                    dependent: :destroy
end
