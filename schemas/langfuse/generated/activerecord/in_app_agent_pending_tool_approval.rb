class InAppAgentPendingToolApproval < ApplicationRecord
  self.primary_key = %w[project_id conversation_id tool_call_id]

  validates :approval_fingerprint, presence: true
  validates :expires_at, presence: true

  belongs_to :project, inverse_of: :in_app_agent_pending_tool_approval
  belongs_to :conversation, class_name: "InAppAgentConversation", foreign_key: %i[conversation_id project_id],
                            inverse_of: :pending_tool_approvals
end
