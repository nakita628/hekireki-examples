defmodule Documenso.DocumentAuditLog do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Documenso.PrismaDateTime, autogenerate: {Documenso.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          type: String.t(),
          data: map(),
          name: String.t() | nil,
          email: String.t() | nil,
          user_id: integer() | nil,
          user_agent: String.t() | nil,
          ip_address: String.t() | nil,
          inserted_at: DateTime.t(),
          envelope: Documenso.Envelope.t() | nil
        }

  schema "DocumentAuditLog" do
    field(:id, :string, primary_key: true)
    field(:type, :string)
    field(:data, :map)
    field(:name, :string)
    field(:email, :string)
    field(:user_id, :integer, source: :userId)
    field(:user_agent, :string, source: :userAgent)
    field(:ip_address, :string, source: :ipAddress)
    field(:envelope_id, :string, source: :envelopeId)
    belongs_to(:envelope, Documenso.Envelope, foreign_key: :envelope_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end