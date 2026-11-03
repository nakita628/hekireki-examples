defmodule Documenso.Session do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Documenso.PrismaDateTime, autogenerate: {Documenso.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          session_token: String.t(),
          ip_address: String.t() | nil,
          user_agent: String.t() | nil,
          expires_at: DateTime.t(),
          is_embed: boolean(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: Documenso.User.t() | nil
        }

  schema "Session" do
    field(:id, :string, primary_key: true)
    field(:session_token, :string, source: :sessionToken)
    field(:ip_address, :string, source: :ipAddress)
    field(:user_agent, :string, source: :userAgent)
    field(:expires_at, Documenso.PrismaDateTime, source: :expiresAt)
    field(:is_embed, :boolean, default: false, source: :isEmbed)
    field(:user_id, :id, source: :userId)
    belongs_to(:user, Documenso.User, foreign_key: :user_id, define_field: false)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end