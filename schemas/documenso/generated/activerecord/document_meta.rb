class DocumentMeta < ApplicationRecord
  self.table_name = "DocumentMeta"

  attribute :id, default: -> { Cuid.generate }
  attribute :timezone, default: "Etc/UTC"
  attribute :dateFormat, default: "yyyy-MM-dd hh:mm a"
  attribute :allowDictateNextSigner, default: false
  attribute :typedSignatureEnabled, default: true
  attribute :uploadSignatureEnabled, default: true
  attribute :drawSignatureEnabled, default: true
  attribute :language, default: "en"

  enum :signingOrder, { parallel: "PARALLEL", sequential: "SEQUENTIAL" }, default: :parallel, validate: true
  enum :distributionMethod, { email: "EMAIL", none: "NONE" }, default: :email, prefix: true, validate: true

  has_one :envelope, foreign_key: "documentMetaId", inverse_of: :document_meta, dependent: :restrict_with_error
end
