class Action < ApplicationRecord
  self.inheritance_column = nil
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }

  enum :type, { webhook: "WEBHOOK", slack: "SLACK", github_dispatch: "GITHUB_DISPATCH", pagerduty: "PAGERDUTY" },
       validate: true

  validates :config, exclusion: { in: [ nil ] }

  belongs_to :project, inverse_of: :actions
  has_many :automations, dependent: :destroy
  has_many :automation_executions, dependent: :destroy
end
