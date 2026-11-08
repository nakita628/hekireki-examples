class WorkflowStep < ApplicationRecord
  self.inheritance_column = nil
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :type, { trigger: "TRIGGER", send_email: "SEND_EMAIL", delay: "DELAY", wait_for_event: "WAIT_FOR_EVENT", condition: "CONDITION", exit: "EXIT", webhook: "WEBHOOK", update_contact: "UPDATE_CONTACT" },
       suffix: true, validate: true

  validates :name, presence: true
  validates :position, exclusion: { in: [ nil ] }
  validates :config, exclusion: { in: [ nil ] }

  belongs_to :workflow, foreign_key: "workflowId", inverse_of: :steps
  belongs_to :template, foreign_key: "templateId", optional: true, inverse_of: :workflow_steps
  has_many :outgoing_transitions, class_name: "WorkflowTransition", foreign_key: "fromStepId", inverse_of: :from_step,
                                  dependent: :destroy
  has_many :incoming_transitions, class_name: "WorkflowTransition", foreign_key: "toStepId", inverse_of: :to_step,
                                  dependent: :destroy
  has_many :executions, class_name: "WorkflowStepExecution", foreign_key: "stepId", inverse_of: :step,
                        dependent: :destroy
  has_many :current_workflow_executions, class_name: "WorkflowExecution", foreign_key: "currentStepId",
                                         inverse_of: :current_step, dependent: :nullify
end
