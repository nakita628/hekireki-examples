class ReportIssue < ApplicationRecord
  self.table_name = "ReportIssue"
  self.inheritance_column = nil

  enum :type, { derogatory: "DEROGATORY", other: "OTHER", unclear: "UNCLEAR", bullying: "BULLYING", spam: "SPAM", hatespeech: "HATESPEECH", threat: "THREAT" },
       validate: true

  validates :id, uniqueness: true

  belongs_to :report, foreign_key: "reportId", inverse_of: :issues
end
