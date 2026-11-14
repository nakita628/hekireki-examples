class SelfHostedInstanceDiagnostic < ApplicationRecord
  self.table_name = "SelfHostedInstanceDiagnostic"

  validates :instanceId, presence: true
  validates :stepId, presence: true
  validates :attempt, presence: true
  validates :trigger, presence: true
  validates :byteLength, presence: true
  validates :objectKey, presence: true
  validates :expiresAt, presence: true
end
