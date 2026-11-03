defmodule Papermark.AgreementResponse do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Papermark.PrismaDateTime, autogenerate: {Papermark.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          link_id: String.t() | nil,
          signer_email: String.t() | nil,
          signer_name: String.t() | nil,
          signing_status: String.t(),
          signing_external_id: String.t() | nil,
          signing_envelope_id: String.t() | nil,
          signing_document_id: integer() | nil,
          signed_at: DateTime.t() | nil,
          completed_at: DateTime.t() | nil,
          signed_file_key: String.t() | nil,
          signed_file_name: String.t() | nil,
          signed_file_storage_type: atom() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          agreement: Papermark.Agreement.t() | nil,
          view: Papermark.View.t() | nil
        }

  schema "AgreementResponse" do
    field(:id, :string, primary_key: true)
    field(:link_id, :string, source: :linkId)
    field(:signer_email, :string, source: :signerEmail)
    field(:signer_name, :string, source: :signerName)
    field(:signing_status, :string, default: "PENDING", source: :signingStatus)
    field(:signing_external_id, :string, source: :signingExternalId)
    field(:signing_envelope_id, :string, source: :signingEnvelopeId)
    field(:signing_document_id, :integer, source: :signingDocumentId)
    field(:signed_at, Papermark.PrismaDateTime, source: :signedAt)
    field(:completed_at, Papermark.PrismaDateTime, source: :completedAt)
    field(:signed_file_key, :string, source: :signedFileKey)
    field(:signed_file_name, :string, source: :signedFileName)
    field(:signed_file_storage_type, Ecto.Enum, values: [:S3_PATH, :VERCEL_BLOB], source: :signedFileStorageType)
    field(:agreement_id, :string, source: :agreementId)
    field(:view_id, :string, source: :viewId)
    belongs_to(:agreement, Papermark.Agreement, foreign_key: :agreement_id, define_field: false, type: :string)
    belongs_to(:view, Papermark.View, foreign_key: :view_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end